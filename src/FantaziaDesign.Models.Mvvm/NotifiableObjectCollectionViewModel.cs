using FantaziaDesign.Core;
using System.Collections.Generic;

namespace FantaziaDesign.Models.Mvvm
{
	public class NotifiableObjectCollectionViewModel<TNotifiableObject> : NotifiableObjectCollection<TNotifiableObject>, IReadOnlyKeyStorage<ResourceTemplateType, string> where TNotifiableObject : INotifiableObject
	{
		protected string m_styleTemplateNameKey;
		protected string m_contentTemplateNameKey;
		protected string m_containerTemplateNameKey;

		public NotifiableObjectCollectionViewModel() : base()
		{
		}

		public NotifiableObjectCollectionViewModel(IEnumerable<TNotifiableObject> collection) : base(collection)
		{
		}

		public NotifiableObjectCollectionViewModel(List<TNotifiableObject> list) : base(list)
		{
		}

		public virtual string GetKey(ResourceTemplateType keyDescription)
		{
			switch (keyDescription)
			{
				case ResourceTemplateType.ContentTemplate:
					return m_contentTemplateNameKey;
				case ResourceTemplateType.ContainerTemplate:
					return m_containerTemplateNameKey;
				case ResourceTemplateType.StyleTemplate:
					return m_styleTemplateNameKey;
				default:
					return null;
			}
		}
	}
}
