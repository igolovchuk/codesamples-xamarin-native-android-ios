using System;
using XamarinNativeFullDemo.Extensions;

namespace XamarinNativeFullDemo.Models
{
    [Serializable]
    public class Account
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public bool IsFemale { get; set; }

        public int A { get; set; }

		public int B { get; set; }

		public int C { get; set; }

		public int D { get; set; }

		public int E { get; set; }

		public int F { get; set; }

		public int G { get; set; }

		public int H { get; set; }

        public Account(string name,bool isFemale, int a, int b, int c, int d, int e, int f, int g, int h)
        {
            Name = name;
            UserName = name.ToUserName();
			IsFemale = isFemale;
            Icon = @"default_account.png";
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            G = g;
            H = h;
        }
    }
}
