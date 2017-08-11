#include <SDL.h>
// required to load png file
#include <SDL_image.h>
// required to create labels
#include <SDL_ttf.h>
#include <cstdlib>
// required to std::cout
#include <iostream>
// required to std::to_string()
#include <string>

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
	TTF_Font *font;

	// Initialising
	imageSurface = IMG_Load("Resources/wabbit_alpha.png");
	IMG_Init(IMG_INIT_PNG);
	TTF_Init();

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
			// sets type of font
			font = TTF_OpenFont("Resources/arial.ttf", 25);
			// sets color of font
			SDL_Color color = { 255,255,255 };
			// sets surfaces for labels
			SDL_Surface *resultSurface = TTF_RenderText_Solid(font, "Rabbits: 0", color);
			SDL_Surface *fpsSurface = TTF_RenderText_Solid(font, "FPS: 60", color);
			// sets textures for labels
			SDL_Texture *resultTexture = SDL_CreateTextureFromSurface(renderer, resultSurface);
			SDL_Texture *fpsTexture = SDL_CreateTextureFromSurface(renderer, fpsSurface);
			// sets rectangles to display
			// { positionX, positionY, width, height }
			SDL_Rect resultRect = { 0,0,150,30 };
			SDL_Rect fpsRect = { 0,570,150,30 };
			// creates texture from surface
			SDL_Texture *texture = SDL_CreateTextureFromSurface(renderer, imageSurface);
			// free memory
			SDL_FreeSurface(resultSurface);
			SDL_FreeSurface(fpsSurface);
			SDL_FreeSurface(imageSurface);
			// sets rect of displaying image
			SDL_Rect rect = { 390,290,20,32 };
			// sets direction of moving image
			int direction = 1;
			int rabbits = 0;
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
					std::string result;
					// concatenating
					result = "FPS: " + std::to_string(frameCounter);
					// to update text we must create new surface and then texture
					fpsSurface = TTF_RenderText_Solid(font, result.c_str(), color);
					fpsTexture = SDL_CreateTextureFromSurface(renderer, fpsSurface);
					// free memory
					result = "Rabbits: " + std::to_string(rabbits);
					resultSurface = TTF_RenderText_Solid(font, result.c_str(), color);
					resultTexture = SDL_CreateTextureFromSurface(renderer, resultSurface);
					SDL_FreeSurface(resultSurface);
					SDL_FreeSurface(fpsSurface);
					// reset counters
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
						SDL_DestroyTexture(resultTexture);
						SDL_DestroyTexture(fpsTexture);
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
				SDL_RenderCopy(renderer, fpsTexture, NULL, &fpsRect);
				SDL_RenderCopy(renderer, resultTexture, NULL, &resultRect);
				// show prepared scene
				SDL_RenderPresent(renderer);
				// increase counter of frames every frame
				frameCounter += 1;
			}
		}
	}

	TTF_CloseFont(font);
	// Destroy renderer
	SDL_DestroyRenderer(renderer);

	// Destroy window
	SDL_DestroyWindow(window);
	window = NULL;

	// Quit SDL subsystems
	IMG_Quit();
	TTF_Quit();
	SDL_Quit();
	return 0;
}