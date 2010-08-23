using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Dominion.Specs.Bindings
{
    // Borrowed from Orchard.
    // http://orchard.codeplex.com/SourceControl/changeset/view/c0577b633ffa#src%2fOrchard.Specs%2fBindings%2fBindingBase.cs
    public class BindingBase
    {
        protected static T Binding<T>()
        {
            return (T)ScenarioContext.Current.GetBindingInstance(typeof(T));
        }

        protected Table TableData<T>(params T[] rows)
        {
            return BuildTable(rows);
        }

        protected Table TableData<T>(IEnumerable<T> rows)
        {
            return BuildTable(rows);
        }

        private Table BuildTable<T>(IEnumerable<T> rows)
        {
            var properties = typeof(T).GetProperties();
            var table = new Table(properties.Select(x => x.Name).ToArray());
            foreach (var row in rows)
            {
                var row1 = row;
                table.AddRow(properties.Select(p => Convert.ToString(p.GetValue(row1, null))).ToArray());
            }
            return table;
        }
    }
}