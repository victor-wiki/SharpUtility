using System;
using System.Collections.Generic;
using System.Text;

namespace SharpUtility.Helper
{
    public class TupleHelper
    {
        public static List<object> GetTupleItems(object tuple)
        {
            List<object> list = new List<object>();

            var fields = tuple.GetType().GetFields();

            foreach (var field in fields)
            {
                list.Add(field.GetValue(tuple));
            }

            return list;
        }
    }
}
