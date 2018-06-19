using Assets.Commands;
using Assets.Models;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Assets.Signals;
using UnityEngine;
using Assets.View;


public class AppContext : MVCSContext
{
    public AppContext (MonoBehaviour view) : base(view)
    {
        _instance = this;
    }

    public AppContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
        _instance = this;
    }

    private static AppContext _instance;

    public static T Get<T>()
    {
        return _instance.injectionBinder.GetInstance<T>();
    }
		
    // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }
		
    // Override Start so that we can fire the StartSignal 
    public override IContext Start()
    {
        base.Start();

        var startSignal= injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        
        return this;
    }
		
    protected override void mapBindings()
    {
        injectionBinder.Bind<StartSignal>().ToSingleton();
        injectionBinder.Bind<ImageLoadedSignal>().ToSingleton();
        injectionBinder.Bind<LoadImageSignal>().ToSingleton();
        injectionBinder.Bind<RoomsDataLoadedSignal>().ToSingleton();
        injectionBinder.Bind<ImageLoadErrorSignal>().ToSingleton();
        injectionBinder.Bind<ShowPopupSignal>().ToSingleton();
        injectionBinder.Bind<RoomsFetchedSignal>().ToSingleton();
        injectionBinder.Bind<ICacheService>().To<BinaryCacheService>().ToSingleton();
        injectionBinder.Bind<Data>().ToSingleton();
        injectionBinder.Bind<GameState>().ToSingleton();

        
        commandBinder.Bind<StartSignal>().To<FetchRoomsCommands>().Once();
        commandBinder.Bind<LoadImageSignal>().To<DownloadImageByUrlCommand>();

        mediationBinder.Bind<JoinRoomPopup>().To<BasePopupMediator>();
        mediationBinder.Bind<PopupView>().To<PopupViewMediator>();
        mediationBinder.Bind<RoomsView>().To<RoomsViewMediator>();
        mediationBinder.Bind<AsyncImageView>().To<AsyncImageMediator>();
        
        
    }
}


