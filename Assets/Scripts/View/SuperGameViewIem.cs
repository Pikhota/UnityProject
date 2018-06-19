using System.Linq;
using Assets.Models;
using TMPro;
using UnityEngine;

namespace Assets.View
{
    public class SuperGameViewIem : ViewWithModel<SuperGame>
	{
		[SerializeField] private AsyncImageView _coinsIcon;
		[SerializeField] private TextMeshProUGUI _coinValue;
		[SerializeField] private ProgressBarWithText _progressBar;
		
		public override void Init(SuperGame _model)
		{
			base.Init(_model);
			
			_progressBar.Fill(_model.Current, _model.Max);
			_coinsIcon.InitUrl(AppContext.Get<Data>().Settings.First(p => p.Name == Constants.CoinsIcon).Value);
			_coinValue.text = _model.Price.ToString();

		}
	}
}