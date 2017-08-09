cc.Class({
    extends: cc.Component,
    
    // variables of this class
    properties: {
        // enabling and disabling adding new sprites
        add: false,
        // current number of rabbits in the scene
        numberOfRabbits: 0,
        // prefab which will be instantiating
        rabbitPrefab: cc.Prefab,
        parentNode: {
            default: null,
            type: cc.Node,
        },
        resultLabel: {
            default: null,
            type: cc.Label,
        }
    },

    // use this for initialization
    onLoad: function () {
        var self = this;
        cc.eventManager.addListener({
            event: cc.EventListener.TOUCH_ONE_BY_ONE,
            onTouchBegan: function(touch,event){
                return true;
            },
            onTouchMoved: function(touch,event){
                return true;
            },
            onTouchEnded: function(touch,event){
                self.add = true;
            }
        },this.node);
    },

    // called every frame, uncomment this function to activate update callback
    update: function (dt) {
        if(this.add){
            this.numberOfRabbits += 25;
            var node;
            this.resultLabel.string = "Rabbits: " + this.numberOfRabbits;
            for(var i=0; i<25; i++){
                node = cc.instantiate(this.rabbitPrefab);
                node.parent = this.parentNode;
            }
            this.add = false;
        }
    },
});
