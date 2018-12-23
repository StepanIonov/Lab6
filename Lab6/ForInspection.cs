namespace Lab6
{
    public class ForInspection
    {
        public ForInspection() { }
        public ForInspection(int i) { }
        public ForInspection(string str) { }

        public double Plus(double x, double y) { return x + y; }
        public double Minus(double x, double y) { return x - y; }

        [NewAttribute(Description = "Описание для Property1")]
        public string Property1 { get; set; }

        [NewAttribute(Description = "Описание для Property2")]
        public double Property2 { get; private set; }

        public float Property3 { get; private set; }
    }
}
