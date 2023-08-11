using ClassLibrary1;
using Realms;

namespace RealmLibrary
{
    public static class RealmConfig
    {
        private readonly static RealmConfiguration AppConfig =
            new("t1.realm")
            {
                IsReadOnly = true,
                Schema = new[]
                    {
                        typeof(RFoo),
                        typeof(RBar)
                    },
            };

        public static Realm GetAppReadOnlyRealm() => Realm.GetInstance(AppConfig);
    }

    internal partial class RFoo : IRealmObject
    {
        [PrimaryKey]
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public static RFoo FromFoo(Foo f, Realm realm)
        {
            var rf = realm.Find<RFoo>(f.Key);
            if (rf != null) { return rf; }
            else
            {
                rf = new RFoo
                {
                    Key = f.Key,
                    Value = f.Value
                };
                realm.Add(rf);
                return rf;
            }
        }
    }

    internal partial class RBar : IRealmObject
    {
        [PrimaryKey]
        public Guid Guid { get; set; }
        public IList<RFoo> Xs { get; } = null!;

        public static RBar FromCourse(Bar b, Realm realm)
        {
            var r_c = new RBar { Guid = b.Guid };
            foreach (Foo f in b.Xs)
            {
                r_c.Xs.Add(RFoo.FromFoo(f, realm));
            }
            realm.Add(r_c);
            return r_c;
        }
    }
}
