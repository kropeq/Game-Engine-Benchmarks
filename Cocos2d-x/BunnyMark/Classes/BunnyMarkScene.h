#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"

class BunnyMark : public cocos2d::Scene
{
public:
    static cocos2d::Scene* createScene();

    virtual bool init();
	// implement the "static create()" method manually
	CREATE_FUNC(BunnyMark);
	void update(float) override;
	int direction = 1;
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);
    
    

	cocos2d::Sprite* rabbit;

};

#endif // __HELLOWORLD_SCENE_H__
