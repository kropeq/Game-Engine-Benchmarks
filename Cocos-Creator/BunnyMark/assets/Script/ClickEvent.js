cc.Class({
    extends: cc.Component,
    
    // variables of this class
    properties: {
        // current number of rabbits in the scene
        numberOfRabbits: 0,
        // stores all rabbits
        list: [],
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
                // increase result by 25
                self.numberOfRabbits += 25;
                var node;
                // update the UI Text with new result
                self.resultLabel.string = "Rabbits: " + self.numberOfRabbits;
                // add the next rabbits
                for(var i=0; i<25; i++){
                     // cloning prefab
                    node = cc.instantiate(self.rabbitPrefab);
                    // random position of the sprite
                    node.x = Math.floor((Math.random()* 740 ) + 30 );
                    node.y = Math.floor((Math.random()* 560 ) + 10 );
                    // adding to the parent folder
                    node.parent = self.parentNode;
                    // adding to list
                    self.list.push({direction: 1, rabbit: node});
                }
            }
        },this.node);
    },

    // called every frame
    update: function (dt) {
        // lets update x and y position of all bunnies
        this.list.forEach(function(entry){
            entry.rabbit.y -= 5*entry.direction;
            if(entry.rabbit.y < 20 ){
                entry.direction = -1;
                entry.rabbit.y = 20;
            }
            if(entry.rabbit.y > 580 ){
                entry.direction = 1;
                entry.rabbit.y = 580;
            }
        })
    },
});
