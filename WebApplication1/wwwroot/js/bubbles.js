
var bubbleOptions = {
    timer: -1,    //The interval time
    tick: 100,   //The tick speed
    bubbles: [],    //The array of bubbles

    update: function (tick) { 
        if (!document.addEventListener) return;
        window.clearInterval(this.timer);
        if (!tick) tick = this.tick;
        if (!this.bubbles.length) this.bubbles = instantiateBubbles();
        var bubbles = this.bubbles;
        this.timer = window.setInterval(
            function () {
                for (var i = 0; i < bubbles.length; i++)
                    bubbles[i].move();
            },
            tick
        );
    }
};

if (window.onload) {
    windowOnLoad = window.onload;
    window.onload = function () {
        windowOnLoad();
        bubbleOptions.update();
    }
} else window.onload = function () { bubbleOptions.update(); };

function instantiateBubbles() {
    //The bubbles need a container element
    var bubbleContainer = document.getElementById("bubbleContainer");
    if (!bubbleContainer) {
        bubbleContainer = document.createElement('div');
        bubbleContainer.setAttribute('id', 'bubbleContainer');
        document.body.appendChild(bubbleContainer);
    }
    //Create the bubbles
    var bubbles = [];
    var bubbleLength = Math.floor(window.innerWidth * window.innerHeight / 45000);
    for (var i = 0; i < bubbleLength; i++) {
        var bubbleElem = document.createElement('div');
        bubbleElem.setAttribute('id', 'bubble' + i);
        bubbleContainer.appendChild(bubbleElem);
        bubbles.push(new Bubble(bubbleElem));
    }
    //Return the array of bubbles
    return bubbles;
}

function Bubble(element) {
    this.x = 0;			
    this.y = 0;			
    this.xVel = 0;		
    this.yVel = 0;		
    this.time = 0;		
    this.e = element;	
    this.diam = 0;		

    this.create();
}

//Style the bubble
Bubble.prototype.create = function () {
    //Reset counter
    this.time = 0;

    //Position
    this.x = Math.random() * window.innerWidth;
    this.y = Math.random() * window.innerHeight;

    //Random velocity
    this.xVel = (Math.random() * 4) - 2;
    this.yVel = (Math.random() * 4) - 2;

    //Set the size
    this.diam = Math.floor(Math.random() * 150) + 40;
    this.e.style.width = this.diam + "px";
    this.e.style.height = this.diam + "px";

    //Set the color
    var hue = Math.floor(Math.random() * 20) + 188;
    var saturation = Math.floor(Math.random() * 10)+ 15;
    var light = Math.floor(Math.random() * 10) +  70;
    var opacity = Math.min(Math.max(Math.random() / 3, 0.1), 1);

    var hsla = "hsla(" + hue + "," + saturation + "%," + light + "%," + opacity + ")";
    this.e.style.backgroundColor = hsla;

    //Set the glow
    this.e.style.boxShadow = "0 0 " + (Math.floor(Math.random() * 10) + 5)
        + "px " + hsla;
    this.e.style.opacity = "0";
}

//Move the bubble
Bubble.prototype.move = function () {
    //If out of the window bounds recreate the bubble
    if (this.x + this.diam < 0 || this.x > window.innerWidth ||
        this.y + this.diam < 0 || this.y - this.diam > window.innerHeight) {
        this.create();
    } else {
        if (this.time < 11) this.e.style.opacity = (this.time / 10);

        this.x += this.xVel;
        this.y += this.yVel;

        this.e.style.left = Math.floor(this.x) + "px";
        this.e.style.top = Math.floor(this.y) + "px";

        this.time++;
    }
}