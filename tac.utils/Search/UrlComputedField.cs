using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAC.Utils.Search 
{
    abstract class UrlComputedField : AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable ind)
        {
            object result = new object();

            if (ind.GetType() == typeof(SitecoreIndexableItem))
            {
                ind = (SitecoreIndexableItem)ind;
                Uri v = new Uri(ind.AbsolutePath);

                return v;
            }

            return result;
        }


    }
}
