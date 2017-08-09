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
        // node that contains all of the rabbit sprites
        parentNode: {
            default: null,
            type: cc.Node,
        },
        // text label of result
        resultLabel: {
            default: null,
            type: cc.Label,
        }
    },

    // use this for initialization
    onLoad: function () {
        cc.director.setDisplayStats(true);
        // taking variables outside
        var self = this;
        // click and touch screen listener
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
        // if user click the screen
        if(this.add){
            // increase result by 25
            this.numberOfRabbits += 25;
            var node;
            // update the UI Text with new result
            this.resultLabel.string = "Rabbits: " + this.numberOfRabbits;
            // add the next rabbits
            for(var i=0; i<25; i++){
                // cloning prefab
                node = cc.instantiate(this.rabbitPrefab);
                // adding to the parent folder
                node.parent = this.parentNode;
            }
            // disable adding rabbits
            this.add = false;
        }
    },
});
