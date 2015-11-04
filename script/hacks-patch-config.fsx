#r "../tools/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "System.Xml.Linq.dll"

open FSharp.Data
open System
open System.IO
open System.Xml
open System.Xml.Linq

let assemblyName = "System.Threading.Tasks.Dataflow"
let destination = Path.Combine(
                      __SOURCE_DIRECTORY__,
                      "../tools/FAKE.Core/tools/FAKE.exe.config")

// parse the existing configuration
type InputXml = XmlProvider<"../tools/FAKE.Core/tools/FAKE.exe.config">

// element to be inserted into the dependentAssembly array
let dataFlowAssembly =
    let identity = InputXml.AssemblyIdentity(assemblyName, "b03f5f7f11d50a3a", "neutral")
    let bindingRedirects = [| InputXml.BindingRedirect("0.0.0.0-4.5.25.0", "4.5.25.0") |]
    InputXml.DependentAssembly(identity, bindingRedirects)

let rootElement = InputXml.GetSample()
let startup = rootElement.Startup
let runtime = rootElement.Runtime

// filter out all the other assemblies (in case this script is re-run)
let otherDependentAssemblies =
  runtime.AssemblyBinding.DependentAssemblies
   |> Seq.filter  (fun i -> i.AssemblyIdentity.Name <> assemblyName)
   |> Seq.toList

// HACK HACK HACK
// need to recreate the XML document as existing elements don't
// serialize to disk correctly - only the newly-created element
let newDependencies = InputXml.AssemblyBinding [|
    for b in List.append otherDependentAssemblies [ dataFlowAssembly ] do
        let identity = InputXml.AssemblyIdentity(b.AssemblyIdentity.Name, b.AssemblyIdentity.PublicKeyToken, b.AssemblyIdentity.Culture)
        let redirects =
            b.BindingRedirects
                |> Seq.map(fun b -> InputXml.BindingRedirect(b.OldVersion, b.NewVersion))
                |> Seq.toArray
        yield InputXml.DependentAssembly(identity, redirects)
|]

let newDocument =
    InputXml.Configuration(
        InputXml.Startup(
            startup.UseLegacyVRuntimeActivationPolicy,
            InputXml.SupportedRuntime(
                startup.SupportedRuntime.Version,
                startup.SupportedRuntime.Sku)),
        InputXml.Runtime(
            InputXml.LoadFromRemoteSources(runtime.LoadFromRemoteSources.Enabled),
            newDependencies))

// write the file back out to disk
let writer = XmlWriter.Create(destination)
newDocument.XElement.WriteTo(writer)
writer.Flush()
