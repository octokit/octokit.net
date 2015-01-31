# Debugging Octokit Source in your app

As of version 0.6, Octokit supports symbol debugging - this enables you to step
into the Octokit source without having the repository on your local machine.

### Enable Symbol Debugging

To enable this, you need to enable this in VS:

 - From the top **Tools** menu, select **Options**
 - Select **Debugging** from the left sidebar and expand **General**

Ensure you have checked **Enable source server support**:

![Enable source server support](https://cloud.githubusercontent.com/assets/359239/5388961/31f9b29e-8144-11e4-8c47-08aca6dee697.png)

### How does this actually work?

When you hit a breakpoint in your application, you can step into the
Octokit source code using `F11`. This will retrieve the source file associated
with a specific type, and cache it in your local symbols cache.

![F11 step into symbol](https://cloud.githubusercontent.com/assets/359239/5389259/74600502-8149-11e4-94f7-10dc79a0573f.gif)

You can then set subsequent breakpoints inside the source code, to return to 
in the debugging session.

### Acknowledgements

Thanks to [Cameron Taggart](http://blog.ctaggart.com/) for building the
[SourceLink](https://github.com/ctaggart/SourceLink) framework which we use
to support this behaviour.