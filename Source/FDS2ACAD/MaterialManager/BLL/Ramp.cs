namespace MaterialManager.BLL
{
    public class Ramp
    {
        private Ramp() { }

        public Ramp(string identifier, double timeSpan, double functionValue)
        {
            Identifier = identifier;
            TimeSpan = timeSpan;
            FunctionValue = functionValue;
        }

        public string Identifier { get; set; }

        public double TimeSpan { get; set; }

        public double FunctionValue { get; set; }
    }
}