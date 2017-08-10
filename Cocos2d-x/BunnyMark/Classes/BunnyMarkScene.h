#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
#include <list>

// required to define structure below
using namespace cocos2d;

// structure with direction of moving and which sprite it is
struct RabbitObj {
	int direction;
	Sprite *rabbit;
};

class BunnyMark : public cocos2d::Scene
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
	void addRabbits();

	std::list<RabbitObj> rabbits;

	// a selector callback
	void menuCloseCallback(cocos2d::Ref* pSender);

	// implement the "static create()" method manually
	CREATE_FUNC(BunnyMark);

	void update(float) override;  
	
private:
	float rabbit_width, rabbit_height;
	int minX, maxX, minY, maxY;
	int result = 0;
	int increase = 10;
	cocos2d::Sprite* rabbit;
};

#endif // __HELLOWORLD_SCENE_H__
