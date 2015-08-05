// --------------------------------------------------------------------------------
// Copyright (c) 2015 Ruggero Enrico Visintin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE. 
// --------------------------------------------------------------------------------

console.log("MobileInputManager.js included");

function MobileInputManager(target) {

    if (!"ontouchstart" in document.documentElement) {
        console.log.error("MobileInputManager: not touch device")
        return null;
    }

    var mMouseX;
    var mMouseY;

    var mWheelDelta;
    var mMouseHorizontalDelta = 0;
    var mMouseVerticalDelta = 0;

    var touchesCount = 0;

    this.init = function () {
        target.addEventListener('touchstart', handleMouseDown, false);
        target.addEventListener('touchend', handleMouseUp, false);
        target.addEventListener('touchmove', handleMouseMove, false);
        target.addEventListener('gesturechange', handleWheelDelta, false);
    };

    this.postUpdate = function () {
        mWheelDelta             = 0;
        mMouseHorizontalDelta   = 0;
        mMouseVerticalDelta     = 0;
    };

    this.getWheelDelta = function () {
        return mWheelDelta;
    };

    this.getMousePosition = function () {
        return { x: mMouseX, y: mMouseY };
    };

    this.isLeftMouseDown = function () {
        return touchesCount == 1;
    };

    this.isRightMouseDown = function () {
        return touchesCount == 2;
    };

    this.isWheelMouseDown = function () {
        return mWheelMouseDown;
    };

    this.getMouseHorizontalDelta = function () {
        return mMouseHorizontalDelta;
    };

    this.getMouseVerticalDelta = function () {
        return mMouseVerticalDelta;
    };

    var handleMouseDown = function (event) {
        touchesCount++;

        mMouseX = event.touches[0].pageX;
        mMouseY = event.touches[0].pageY;

        event.preventDefault();
    };

    var handleMouseUp = function (event) {
        touchesCount--;
    };

    var handleMouseMove = function (event) {       
        mMouseHorizontalDelta = (mMouseX - event.touches[0].pageX) * 0.5;
        mMouseVerticalDelta = (mMouseY - event.touches[0].pageY) * 0.5;

        /*if (touchesCount == 2) {
            
            mWheelDelta = Math.sqrt(
                (event.touches[0].pageX - event.touches[1].pageX) * (event.touches[0].pageX - event.touches[1].pageX) +
                (event.touches[0].pageX - event.touches[1].pageX) * (event.touches[0].pageX - event.touches[1].pageX)) * 0.001;

            

            if (mWheelDelta != 0) {
                mMouseHorizontalDelta = 0;
                mMouseVerticalDelta = 0;
            }
        }*/
         
        mMouseX = event.touches[0].pageX;
        mMouseY = event.touches[0].pageY;

        event.preventDefault();
    };

    var handleWheelDelta = function (event) {
        
    };

    return this;
}
