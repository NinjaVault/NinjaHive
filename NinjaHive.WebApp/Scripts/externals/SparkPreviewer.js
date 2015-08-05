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

console.log("SparkViewer.js included");

JRV.setBasePath("../Scripts/externals/");

JRV.include("core/Renderer.js");
JRV.include("core/math/Matrix4.js");
JRV.include("core/RendererUtils.js");
JRV.include("core/RenderMaterial.js");
JRV.include("core/RenderMesh.js");
JRV.include("core/RenderModel.js");
JRV.include("core/math/Vector3.js");

SparkPreviewerMain();

if (JRV.isMobile.any() && JRV.supportTouch()) {
    JRV.include("core/input/MobileInputManager.js");
} else {
    if (JRV.supportMouse()) {
        JRV.include("core/input/PcInputManager.js");
    } else if(JRV.supportTouch()) {
        JRV.include("core/input/MobileInputManager.js");
    }
}

JRV.include("core/Camera.js");
JRV.include("core/utils/ShapeGenerator.js");

function Application(canvas, debugCanvas) {
	var mCanvas = canvas;
	var renderer;
	var debugRenderer;

	var inputManager;

    var backgroundColor;
    var renderModel;

    var litShaderProgram;
    var normalShaderProgram;
    var lightShaderProgram;
    	
	var mModelViewMatrix;
	var mArcballCamera;

	var running;	
	var mFps;
	var mFrameTime;
	var load = false;

	var texture;

	this.init = function () {
        console.log("mobile: " + JRV.isMobile.any());

        renderer = new Renderer();
        renderer.initWebGL(mCanvas);

        debugRenderer = debugCanvas.getContext("2d");

        debugRenderer.fillStyle = "white";
        debugRenderer.font = "11px Lucida Console";        
                
        mArcballCamera = new ArcballCamera();
        mArcballCamera.setViewport(45, mCanvas.clientWidth / mCanvas.clientHeight);

        initBackground();
        initShaderPrograms();
        initDefaultModel();
        initMatrices();
		
        renderer.program = litShaderProgram;		
        renderer.init();

        if (JRV.isMobile.any() && JRV.supportTouch()) {
            inputManager = new MobileInputManager(mCanvas);
        } else {
            if (JRV.supportMouse()) {
                inputManager = new PcInputManager(mCanvas);
            } else if(supportTouch()){
                inputManager = new MobileInputManager(mCanvas);
            }
        }

        inputManager.init();
    };

    this.run = function () {
		running = true;
        runLoop();
    };
	
	this.stop = function () {
		running = false;
	};
	
	var initMatrices = function () {
	    mModelViewMatrix = Matrix4.create();
	};

	var initBackground = function() {
		backgroundColor = Vector3.create(1.0, 1.0, 1.0);
	};
	
	var initShaderPrograms = function () {
	    initShaderFromFile("src/glsl/global.vertex", "src/glsl/diffuse_lit.fragment", renderer.getGfx(), function (result) {
	        litShaderProgram = result;
	    });

	    initShaderFromFile("src/glsl/global.vertex", "src/glsl/normals.fragment", renderer.getGfx(), function (result) {
	        normalShaderProgram = result;
	    });
	};
	
	var initDefaultModel = function() {
	    renderModel = new RenderModel();
	    renderModel.loadFromObj("Edward_Kenway/Edward_Kenway.obj", renderer.getGfx(), function () {
	    //renderModel.loadFromObj("modello_prova/Lara_croft.obj", renderer.getGfx(), function () {
	        var vbo = renderer.getGfx().createBuffer();

	        renderer.getGfx().bindBuffer(renderer.getGfx().ARRAY_BUFFER, vbo);
	        renderer.getGfx().bufferData(renderer.getGfx().ARRAY_BUFFER, new Float32Array(renderModel.getRenderMesh().getVerticesSet()), renderer.getGfx().STATIC_DRAW);

	        renderModel.getRenderMesh().setVertexBufferHandle(vbo);
	        load = true;
	    });
	};
	
	lastLoop = new Date();

	var runLoop = function () {
	    var thisLoop = new Date;

	    mFrameTime = thisLoop - lastLoop;
	    mFps = Math.round(1000 / mFrameTime);

	    updateInput();
	    updateRendering();

        debugRenderer.clearRect(0, 0, 300, 70);
        debugRenderer.fillText("Fps: " + mFps, 10, 10);
        debugRenderer.fillText("Frame Time: " + mFrameTime, 10, 30);

        if (load == false) {
            debugRenderer.fillText("Loading", 10, 50);
        }
	
	    if (running) {
			window.requestAnimationFrame(runLoop);
		} else {
		    return;
		}

		lastLoop = thisLoop;
	};

	window.onresize = function () {
	    console.log("Resizing");
	    mArcballCamera.setViewport(45, mCanvas.clientWidth / mCanvas.clientHeight);
   	};

	var updateRendering = function() 
	{
	    var mvp = Matrix4.create();
	    var camMatrices = mArcballCamera.updateMatrices();

	    Matrix4.multiply(camMatrices[0], camMatrices[1], mvp);
	    Matrix4.multiply(mvp, mModelViewMatrix, mvp);

	    if (load && litShaderProgram) {
	            renderer.startFrame(litShaderProgram);	
	            renderer.getGfx().depthFunc(renderer.getGfx().LESS);

	            for (var i = 0; i < renderModel.getOpaqueMaterials().length; i++) {

	                var drawCall = new DrawCall();
	                drawCall.vbo = renderModel.getRenderMesh().getVertexBufferHandle();
	                drawCall.shaderProgram = litShaderProgram;

	                drawCall.verticesNumber = (renderModel.getOpaqueMaterials()[i].getEndIndex()) * 3;
	                drawCall.verticesStart = (renderModel.getOpaqueMaterials()[i].getStartIndex()) * 3;
	                drawCall.matrixMVP = mvp;

	                drawCall.mvpLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "modelViewProjectionMatrix");
	                drawCall.textureHandle = renderModel.getOpaqueMaterials()[i].getDiffuseTextureHandle();
	                drawCall.textureLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "sampler");
	                drawCall.alphaLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "alpha");
	                
	                drawCall.opacity = renderModel.getOpaqueMaterials()[i].getOpacity();

	                renderer.render(0, drawCall);
	            }

	            //renderer.getGfx().enable(renderer.getGfx().BLEND);
	            //renderer.getGfx().blendFuncSeparate(renderer.getGfx().SRC_ALPHA, renderer.getGfx().ONE_MINUS_SRC_ALPHA, renderer.getGfx().ONE, renderer.getGfx().ONE_MINUS_SRC_ALPHA)

                for (var i = 0; i < renderModel.getTransparentMaterials().length; i++) {
	                var drawCall = new DrawCall();
	                drawCall.vbo = renderModel.getRenderMesh().getVertexBufferHandle();
	                drawCall.shaderProgram = litShaderProgram;

	                drawCall.verticesNumber = renderModel.getTransparentMaterials()[i].getEndIndex() * 3;
	                drawCall.verticesStart = renderModel.getTransparentMaterials()[i].getStartIndex() * 3;
	                drawCall.matrixMVP = mvp;

	                drawCall.mvpLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "modelViewProjectionMatrix");
	                drawCall.textureHandle = renderModel.getTransparentMaterials()[i].getDiffuseTextureHandle();
	                drawCall.textureLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "sampler");
	                drawCall.alphaLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "alpha");

	                drawCall.opacity = renderModel.getTransparentMaterials()[i].getOpacity();
	               
	                renderer.render(0, drawCall);
	            }

	            renderer.endFrame();
	            renderer.getGfx().disable(renderer.getGfx().BLEND);

            }

	    }

	var updateInput = function()
	{
	    if (inputManager.isLeftMouseDown()) {
	        var verticalDelta = inputManager.getMouseVerticalDelta();
	        var horizontalDelta = inputManager.getMouseHorizontalDelta();

            mArcballCamera.rotateY(verticalDelta * 0.02);
            mArcballCamera.rotateX(horizontalDelta * 0.02);
	    } else if (inputManager.isRightMouseDown()) {
	        var horizontalDelta = inputManager.getMouseHorizontalDelta();
	        var verticalDelta = inputManager.getMouseVerticalDelta();

	        mArcballCamera.translateX(horizontalDelta * 0.05);
	        mArcballCamera.translateY(-verticalDelta * 0.05);
	    }

	    if (inputManager.getWheelDelta() != 0) {	        
	        mArcballCamera.moveRadius(-inputManager.getWheelDelta());
	    }

	    inputManager.postUpdate();
	}

	return this;
}

var APPLICATION;

function SparkPreviewerMain() {

    window.onload = function () {
        var debugCanvas = document.getElementById("sparkViewer").cloneNode();

        debugCanvas.id = "sparkDebugger";
        debugCanvas.style.zIndex = "100";
        debugCanvas.style.position = "absolute";
        debugCanvas.style.left = "0";
        debugCanvas.style.top = "0";
        debugCanvas.style.pointerEvents = "none";

        document.body.appendChild(debugCanvas);

        APPLICATION = new Application(document.getElementById("sparkViewer"), debugCanvas);
        APPLICATION.init();
        APPLICATION.run();
    }

    return 0;
}
