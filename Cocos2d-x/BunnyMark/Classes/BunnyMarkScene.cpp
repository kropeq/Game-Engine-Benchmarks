#include "BunnyMarkScene.h"
#include "SimpleAudioEngine.h"

USING_NS_CC;

Scene* BunnyMark::createScene()
{
    return BunnyMark::create();
}

// on "init" you need to initialize your instance
bool BunnyMark::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Scene::init() )
    {
        return false;
    }
    // getting the visible screen size
    auto visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();
    // 2. add a menu item with "X" image, which is clicked to quit the program.
    // add a "close" icon to exit the progress. it's an autorelease object
    auto closeItem = MenuItemImage::create(
                                           "CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(BunnyMark::menuCloseCallback, this));
    
    closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    // create menu, it's an autorelease object
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Vec2::ZERO);
    this->addChild(menu, 1);

    // add a label shows "Hello World"
    // create and initialize a label
    auto label = Label::createWithTTF("BunnyMark v1.0", "fonts/Marker Felt.ttf", 24);
	auto result = Label::createWithTTF("Rabbits: 0", "fonts/Marker Felt.ttf", 24);
    // position the label on the center of the screen
    label->setPosition(Vec2(origin.x + visibleSize.width/2,origin.y + visibleSize.height - label->getContentSize().height));
	result->setPosition(Vec2(origin.x + 80,origin.y + visibleSize.height - result->getContentSize().height));
    // add the label as a child to this layer
	this->addChild(result, 1);
    this->addChild(label, 1);
    // create Rabbit sprite
    rabbit = Sprite::create("wabbit_alpha.png");
	// getting size of this sprite
	rabbit_width = rabbit->getContentSize().width / 2;
	rabbit_height = rabbit->getContentSize().height / 3;
	// checking the edges of the screen
	minX = (int)rabbit_width + 1;
	maxX = (int)(visibleSize.width - rabbit_width) - 1;
	minY = (int)rabbit_height + 1;
	maxY = (int)(visibleSize.height - rabbit_height) - 1;
	// getting random start position of sprite
	int positionX = RandomHelper::random_int(minX, maxX);
	int positionY = RandomHelper::random_int(minY, maxY);
	// setting random start position to sprite
	rabbit->setPosition(Vec2(positionX, positionY));
    // add the sprite as a child to this layer
    this->addChild(rabbit, 0);
	// it enables update function
	this->scheduleUpdate();
    
    return true;
}

void BunnyMark::update(float delta) {
	auto position = rabbit->getPosition();
	position.y -= 5 * direction;
	if (position.y < 10) {
		direction = -1;
		position.y = 10;
	}
	if (position.y > 590) {
		direction = 1;
		position.y = 590;
	}
	rabbit->setPosition(position);
}

void BunnyMark::menuCloseCallback(Ref* pSender)
{
    //Close the cocos2d-x game scene and quit the application
    Director::getInstance()->end();

    #if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
    
    /*To navigate back to native iOS screen(if present) without quitting the application  ,do not use Director::getInstance()->end() and exit(0) as given above,instead trigger a custom event created in RootViewController.mm as below*/
    
    //EventCustom customEndEvent("game_scene_close_event");
    //_eventDispatcher->dispatchEvent(&customEndEvent);
    
    
}
