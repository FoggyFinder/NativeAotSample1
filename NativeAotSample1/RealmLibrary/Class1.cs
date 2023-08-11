using ClassLibrary1;

namespace RealmLibrary
{
    internal partial class RFoo
    {
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public static RFoo FromFoo(Foo f)
        {
            return new RFoo
            {
                Key = f.Key,
                Value = f.Value
            };
        }
    }

    internal partial class RBar
    {
        public Guid Guid { get; set; }
        public IList<RFoo> Xs { get; } = null!;

        public static RBar FromCourse(Bar b)
        {
            var r_c = new RBar { Guid = b.Guid };
            foreach (Foo f in b.Xs)
            {
                r_c.Xs.Add(RFoo.FromFoo(f));
            }
            return r_c;
        }
    }
}
