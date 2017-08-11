#include <SDL.h>
// required to load png file
#include <SDL_image.h>
#include <cstdlib>
// required to std::cout
#include <iostream>

using namespace std;

// setting rendering speed to 60 fps
const int FRAMES_PER_SECOND = 60;

int main(int argc, char *argv[])
{
	// time of frame
	float deltaTime = 0;
	// accumulates time of frames to 1.00s and then reset it and accumulates again
	float frameTime = 0;
	// previous start time of frame
	int prevTime = 0;
	// current start time of frame
	int currentTime = 0;

	float fps = 0;
	// difference between start and end of the same frame
	float thisFrameTime = 0;

	// counter of frames per second
	int frameCounter = 0;

	// while is false, app will update in 60 FPS
	bool quit = false;

	// stores events
	SDL_Event event;
	SDL_Window *window = nullptr;
	// surface for image
	SDL_Surface *imageSurface = nullptr;
	SDL_Renderer *renderer;

	// Initialising
	imageSurface = IMG_Load("Resources/wabbit_alpha.png");
	IMG_Init(IMG_INIT_PNG);

	if (SDL_Init(SDL_INIT_EVERYTHING) < 0)
	{
		std::cout << "SDL could not initalise! SDL Error: " << SDL_GetError() << std::endl;
	}
	else
	{
		if (imageSurface == NULL)
		{
			std::cout << "Image loading Error: " << SDL_GetError() << std::endl;
		}

		// Creates fullscreen window with 800x600 resolution and resizes to fit height
		window = SDL_CreateWindow("SDL BunnyMark",SDL_WINDOWPOS_CENTERED,SDL_WINDOWPOS_CENTERED,800, 600, SDL_WINDOW_FULLSCREEN);
		if (window == NULL) {
			std::cout << "Window creation error: " << SDL_GetError() << std::endl;
		}
		else
		{
			// defines renderer for window
			renderer = SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED);
			SDL_RenderSetLogicalSize(renderer, 800, 600);
			// creates texture from surface
			SDL_Texture *texture = SDL_CreateTextureFromSurface(renderer, imageSurface);
			// free memory
			SDL_FreeSurface(imageSurface);
			// sets rect of displaying image
			SDL_Rect rect;
			rect.x = 390;
			rect.y = 290;
			rect.h = 32;
			rect.w = 20;
			// sets direction of moving image
			int direction = 1;
			// update loop
			while (!quit)
			{
				// counting deltaTime
				prevTime = currentTime;
				currentTime = SDL_GetTicks();
				deltaTime = (currentTime - prevTime) / 1000.0f;
				frameTime += deltaTime;
				fps = 1.0f / deltaTime;
				// every second reset 
				if (frameTime >= 1.00f)
				{
					frameTime = 0;
					frameCounter = 0;
				}

				// event listener
				while (SDL_PollEvent(&event) != 0) {
					switch (event.type)
					{
					// allows finish working the app by alt+F4
					case SDL_QUIT:
						quit = true;
						break;
					}
				}
				// update the position of image
				rect.y += 5 * direction;
				if (rect.y <= 0) {
					rect.y = 0;
					direction = 1;
				}
				if (rect.y >= 570) {
					rect.y = 570;
					direction = -1;
				}
				// clear previous rendered scene
				SDL_RenderClear(renderer);
				// put texture into renderer
				SDL_RenderCopy(renderer, texture, NULL, &rect);
				// delaying time to catch 60 fps tacting
				thisFrameTime = (SDL_GetTicks() - currentTime);
				// if frame lasts shorter than 16,66ms then delay the rest of time
				if (thisFrameTime < 1000 / FRAMES_PER_SECOND)
					{
						SDL_Delay((1000 / FRAMES_PER_SECOND) - thisFrameTime);
					}
				// show prepared scene
				SDL_RenderPresent(renderer);
				// increase counter of frames every frame
				frameCounter += 1;
			}
		}
	}
	// Destroy renderer
	SDL_DestroyRenderer(renderer);

	// Destroy window
	SDL_DestroyWindow(window);
	window = NULL;

	// Quit SDL subsystems
	IMG_Quit();
	SDL_Quit();
	return 0;
}