using FantaziaDesign.Core;

namespace FantaziaDesign.Models.Mvvm
{
	public class NotifiableObjectViewModel : NotifiableObject, IReadOnlyKeyStorage<ResourceTemplateType, string>
	{
		protected string m_styleTemplateNameKey;
		protected string m_contentTemplateNameKey;
		protected string m_containerTemplateNameKey;

		public NotifiableObjectViewModel() : base()
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
