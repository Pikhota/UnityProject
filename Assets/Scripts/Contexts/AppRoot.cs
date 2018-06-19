using strange.extensions.context.impl;

namespace Assets.Contexts
{
    public class AppRoot : ContextView
	{
		private void Awake()
		{
			context = new AppContext(this);
		}
	}
}