namespace Octokit
{
    public class ProductHeaderValue
    {
        readonly System.Net.Http.Headers.ProductHeaderValue _productHeaderValue;

        public ProductHeaderValue(string name)
            : this(new System.Net.Http.Headers.ProductHeaderValue(name))
        {
        }

        public ProductHeaderValue(string name, string value)
            : this(new System.Net.Http.Headers.ProductHeaderValue(name, value))
        {
        }

        ProductHeaderValue(System.Net.Http.Headers.ProductHeaderValue productHeader)
        {
            _productHeaderValue = productHeader;
        }

        public string Name
        {
            get { return _productHeaderValue.Name; }
        }

        public string Version
        {
            get { return _productHeaderValue.Version; }
        }

        public override bool Equals(object obj)
        {
            return _productHeaderValue.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _productHeaderValue.GetHashCode();
        }

        public override string ToString()
        {
            return _productHeaderValue.ToString();
        }

        public static ProductHeaderValue Parse(string input)
        {
            return new ProductHeaderValue(System.Net.Http.Headers.ProductHeaderValue.Parse(input));
        }

        public static bool TryParse(string input,
            out ProductHeaderValue parsedValue)
        {
            System.Net.Http.Headers.ProductHeaderValue value;
            var result = System.Net.Http.Headers.ProductHeaderValue.TryParse(input, out value);
            parsedValue = result ? Parse(input) : null;
            return result;
        }
    }
}