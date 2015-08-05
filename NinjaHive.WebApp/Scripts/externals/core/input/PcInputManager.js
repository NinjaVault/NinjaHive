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

console.log("PcInputManager.js included");

function PcInputManager(target) {   
    var mMouseX;
    var mMouseY;

    var mLeftMouseDown;
    var mRightMouseDown;
    var mWheelMouseDown;

    var mWheelDelta;
    var mMouseHorizontalDelta;
    var mMouseVerticalDelta;

    this.init = function () {
        target.addEventListener('mousedown', handleMouseDown, false);
        target.addEventListener('mouseup', handleMouseUp, false);
        target.addEventListener('mousemove', handleMouseMove, false);
        target.addEventListener('wheel', handleWheelDelta, false);
        target.oncontextmenu = function (e) { e.preventDefault(); };
    };

    this.postUpdate = function () {
        mWheelDelta = 0;
        mMouseHorizontalDelta = 0;
        mMouseVerticalDelta = 0;
    };

    this.getWheelDelta = function () {
        return mWheelDelta;
    };

    this.getMousePosition = function () {
        return { x: mMouseX, y: mMouseY };
    };

    this.isLeftMouseDown = function () {
        return mLeftMouseDown;
    };

    this.isRightMouseDown = function () {
        return mRightMouseDown;
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
        switch (event.which) {
            case 1:
                console.log("LeftMouseDown");
                mLeftMouseDown = true;
                break;
            case 2:
                console.log("MiddleMouseDown");
                mWheelMouseDown = true;
                break;

            case 3:
                console.log("RightMouseDown");
                mRightMouseDown = true;
                break;

        }

        event.preventDefault();
        return false;
    };

    var handleMouseUp = function (event) {
        switch (event.which) {
            case 1:
                console.log("LeftMouseUp");
                mLeftMouseDown = false;
                break;
            case 2:
                console.log("MiddleMouseUp");
                mWheelMouseDown = false;
                break;

            case 3:
                console.log("RightMouseUp");
                mRightMouseDown = false;
                break;

        }

        event.preventDefault();
        return false;
    };

    var handleMouseMove = function (event) {
        console.log("mouse is moving");

        mMouseHorizontalDelta = mMouseX - event.clientX;
        mMouseVerticalDelta = mMouseY - event.clientY;

        mMouseX = event.clientX;
        mMouseY = event.clientY;
    };

    var handleWheelDelta = function (event) {
        mWheelDelta = event.deltaY / 120;
        console.log("wheelIsMoving: " + mWheelDelta);

        event.preventDefault();
    };

    return this;
}