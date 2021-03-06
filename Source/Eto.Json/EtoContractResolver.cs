using System;
using System.IO;
using json = Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Runtime.Serialization;

namespace Eto.Json
{
	public class EtoContractResolver : DefaultContractResolver
	{
		protected override IValueProvider CreateMemberValueProvider (MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Event)
			{
				return new EventValueProvider { EventInfo = (EventInfo) member };
			}
			return base.CreateMemberValueProvider (member);
		}

		protected override System.Collections.Generic.IList<JsonProperty> CreateProperties (Type type, MemberSerialization memberSerialization)
		{
			var list = base.CreateProperties (type, memberSerialization);
			foreach (var eventInfo in type.GetEvents (BindingFlags.Instance | BindingFlags.Public)) {
				var prop = this.CreateProperty (eventInfo, memberSerialization);
				prop.Writable = true;
				list.Add (prop);
			}

			var idprop = type.GetProperty ("ID", BindingFlags.Instance | BindingFlags.Public);
			if (idprop != null) {
				var prop = this.CreateProperty (idprop, memberSerialization);
				prop.PropertyName = "$name";
				prop.PropertyType = typeof(NameConverter.Info);
				prop.MemberConverter = new NameConverter ();
				prop.ValueProvider = new NameConverter.ValueProvider();
				list.Add (prop);
			}
			return list;
		}
	}
	
}
