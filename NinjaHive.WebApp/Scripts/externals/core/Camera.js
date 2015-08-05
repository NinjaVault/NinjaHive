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

console.log("Camera.js included");

JRV.include("core/math/Matrix4.js");
JRV.include("core/math/Vector3.js");

function ArcballCamera() {
    var mPhi                = 90 * Math.PI / 180;
    var mTheta = 0 * Math.PI / 180;
    var position = Vector3.create();
    
    // this is useless junk for now
    // var mTargetX            = 0;  
    // var mTargetY            = 0;

    var mRadius             = 10;

    var mProjectionMatrix   = Matrix4.create();
    var mViewMatrix = Matrix4.create();

    Matrix4.perspective(70, 800 / 600, 0.1, 1000, mProjectionMatrix);

    this.rotateX = function (deltaRads) {
        mTheta += deltaRads;
    };

    // i'll keep it locked till i'll solve matrices multiplication error
    this.rotateY = function (deltaRads) {
        if (deltaRads > 0) {
            if (((mPhi + (deltaRads)) * 180 / Math.PI) < 170) {
                mPhi += (deltaRads);
            }
        } else if (deltaRads < 0) {
            if (((mPhi + (deltaRads)) * 180 / Math.PI) > 10) {
                mPhi += (deltaRads);
            }
        }

        console.log("deltaRads: " + deltaRads * 0.02);
    };

    this.moveRadius = function (delta) {
        var deltaRadius = mRadius - delta;
        console.log("deltaRadius: " + deltaRadius);

        if (deltaRadius > mRadius) {
            if (mRadius < 1000) {
                mRadius += 0.1 * (mRadius - 0);
            }
        } else {
            if (mRadius > 0.1) {
                mRadius -= 0.1 * (mRadius - 0);
            }
        }
    };

    this.updateMatrices = function () {
        var eyeX = mRadius * Math.sin(mTheta) * Math.sin(mPhi) + position[0];
        var eyeY = mRadius * Math.cos(mPhi) + position[1];
        var eyeZ = mRadius * Math.cos(mTheta) * Math.sin(mPhi);

        Matrix4.lookAt([eyeX, eyeY, eyeZ], [position[0], position[1], 0], [0, 1, 0], mViewMatrix);

        return [mProjectionMatrix, mViewMatrix];
    };

    this.translateX = function (translation) {
        //position[0] += translation;
    };

    this.translateY = function (translation) {
        position[1] += translation;
    }

    this.setViewport = function (fov, ratio) {
        Matrix4.perspective(fov, ratio, 0.1, 100, mProjectionMatrix);
    };
};

