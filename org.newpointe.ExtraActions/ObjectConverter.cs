using System;

namespace org.newpointe.WorkflowEntities
{
    public class ObjectConverter
    {
        public static object ConvertObject(String theObject, Type objectType, bool tryToNull = true)
        {
            if (objectType.IsEnum)
            {
                return String.IsNullOrWhiteSpace(theObject) ? null : Enum.Parse(objectType, theObject, true);
            }

            Type underType = Nullable.GetUnderlyingType(objectType);
            if (underType == null) // not nullable
            {
                return Convert.ChangeType(theObject, objectType);
            }

            if (tryToNull && String.IsNullOrWhiteSpace(theObject))
            {
                return null;
            }
            return Convert.ChangeType(theObject, underType);
        }
    }
}
