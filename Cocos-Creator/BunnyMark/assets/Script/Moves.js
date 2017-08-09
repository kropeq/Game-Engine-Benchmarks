cc.Class({
    extends: cc.Component,
    
    // variables of this class
    properties: {
        direction: 1
    },

    // use this for initialization
    onLoad: function () {
        // setting random start position of the sprite
        this.node.x = Math.floor((Math.random() * 740 ) + 30);
        this.node.y = Math.floor((Math.random() * 560 ) + 10);
    },

    // called every frame, uncomment this function to activate update callback
    update: function (dt) {
        // moving this sprite
        this.node.y -= 5*this.direction;
        // if it goes out of the screen
        if(this.node.y < 20 ){
            this.direction = -1;
            this.node.y = 20;
        }
        if(this.node.y > 580 ){
            this.direction = 1;
            this.node.y = 580;
        }
    },
});
