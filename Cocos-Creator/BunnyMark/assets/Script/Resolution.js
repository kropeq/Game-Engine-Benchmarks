cc.Class({
    extends: cc.Component,

    properties: {
    },

    // use this for initialization
    onLoad: function () {
        cc.view.resizeWithBrowserSize(true);
        cc.view.adjustViewPort(true);
        cc.view.setDesignResolutionSize(800, 600, cc.ResolutionPolicy.FIXED_HEIGHT);
        cc.view.setResizeCallback(function(){
           cc.log("RESIZEEE");
           console.log("RESIZEEE");
        });
    },
});
