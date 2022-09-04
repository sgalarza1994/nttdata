using System;

namespace ViewModel
{
    [AttributeUsage(AttributeTargets.All)]
    public class ValidatorAttribute : Attribute
    {
        private bool required;
        public bool Required
        {
            get => required;
            set => required = value;
        }

        private int length;
        public int Length
        {
            get => length;
            set => length = value;

        }

        public ValidatorAttribute(bool required, int length)
        {
            this.required = required;
            this.length = length;
        }
        public ValidatorAttribute(bool required)
        {
            this.required = required;
            length = 0;
        }
    }

}
