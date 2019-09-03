using System;
namespace Parser.ValueConverter
{
    public class MaximumNumberConverter : NumberConverter
    {
		private readonly int maxValue;

        public override int Order => 2;

        public MaximumNumberConverter(int maxValue)
        {
			this.maxValue = maxValue;
		}

		public override int GetValue(string value)
		{
            var val = base.GetValue(value);
			if (val > maxValue)
				return 0;
			return val;
		}
	}
}
