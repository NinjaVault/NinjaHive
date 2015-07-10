{
    var scriptEls = document.getElementsByTagName('script');
    var thisScriptEl = scriptEls[scriptEls.length - 1];
    var scriptPath = thisScriptEl.src;
    var scriptFolder = scriptPath.substr(0, scriptPath.lastIndexOf('/') + 1);

    console.log(scriptFolder);

    var head = document.getElementsByTagName("head")[0];

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/Renderer.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/glMatrix.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/RendererUtils.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/RenderMaterial.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/RenderMesh.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/RenderModel.js";

    head.appendChild(js);

    var js = document.createElement("script");
    js.type = "text/javascript";
    js.src = scriptFolder + "/SparkViewer/RenderTypes.js";

    head.appendChild(js);

    window.onload = SparkPreviewerMain;
}

function Application(canvas) {
	var mCanvas = canvas;
    var renderer;

    var backgroundColor;
    var renderModel;

    var litShaderProgram;
    var normalShaderProgram;
    var diffuseShaderProgram;
    var fullShaderProgram;
	
	var mProjectionMatrix;
	var mModelViewMatrix;

    var LIT_VERTEX_SHADER_SOURCE                            	                     =
        "attribute vec2 position;" 								                     +
		//"attribute vec3 normal;" 								                     +
        "attribute vec2 uv;" 									                     +
		"uniform mat4 modelViewProjectionMatrix;" 				                     +
        "varying vec2 outUv;"									                     +
        "void main(void) {" 									                     +
        "   outUv = uv;"        								                     +
		"   vec4 pos = modelViewProjectionMatrix * vec4(position, 0.0, 1.0);"        +
        "   gl_Position = pos;"       	                                             +
        "}"                                                 	                     ;
	
    var LIT_FRAGMENT_SHADER_SOURCE                          	                     =
        "precision highp float;" 								                     +
        "varying vec2 outUv;" 									                     +
        "void main(void) {"                                 	                     +
        "   gl_FragColor = vec4(1.0, outUv.x, outUv.y, 1);" 	                     +
        "}"                                                 	                     ;

    this.init = function () {
        renderer = new Renderer();
        renderer.initWebGL(mCanvas);
        
        initBackground();
        initShaderPrograms();
        initDefaultModel();
        initMatrices();
		
        renderer.program = litShaderProgram;		
        renderer.init();

        // resize the viewport when the window is resized
        window.onresize = onResizeEvent;
        onResizeEvent(); // force the window to resize to start the application
    };

    this.run = function () {
        runLoop();
    };
	
	var initMatrices = function () {
        mModelViewMatrix = mat4.create();
        mProjectionMatrix = mat4.create();	
		
		mat4.identity(mModelViewMatrix);
		mat4.translate(mModelViewMatrix, [0, 0, -6]);   	
		
        
		//mat4.ortho(-1.0, 1.0, -1.0, 1.0, 0.1, 100, mProjectionMatrix);
        //mat4.scale(mModelViewMatrix, [0.5, 0.5, 0.5]);

	};

	var initBackground = function() {
		backgroundColor = vec3.create(0.0, 0.0, 0.0);
	};
	
	var initShaderPrograms = function () {
		litShaderProgram = initShaderFromString(LIT_VERTEX_SHADER_SOURCE, LIT_FRAGMENT_SHADER_SOURCE, renderer.getGfx());		
	};
	
	var initDefaultModel = function() {
				
        var vertices = [
                0.0, 1.0, 0.0, 1.0,
                -1.0, -1.0, -1.0, -1.0,
                1.0, -1.0, 1.0, -1.0,
        ];
		
		var renderMesh = new RenderMesh();
        var vbo = renderer.getGfx().createBuffer();

        renderer.getGfx().bindBuffer(renderer.getGfx().ARRAY_BUFFER, vbo);
        renderer.getGfx().bufferData(renderer.getGfx().ARRAY_BUFFER, new Float32Array(vertices), renderer.getGfx().STATIC_DRAW);

        renderMesh.setVertexBufferHandle(vbo);
        renderMesh.setVerticesSet(vertices);        

        var renderMaterial = new RenderMaterial();
        var renderMaterialTexture = loadTextureFromUrl("img/img.png", renderer.getGfx());
        renderMaterial.setDiffuseTextureHandle(renderMaterialTexture);

        var materialColor = vec3.create(0.0, 1.0, 0.0);
        renderMaterial.setDiffuseColor(materialColor);

        renderModel = new RenderModel();
        renderModel.setRenderMesh(renderMesh);
        renderModel.addRenderMaterial(renderMaterial);			
	};
	
	var rotation = 1;
	
	var runLoop = function () {
	    var mvp = mat4.create();

		mat4.rotate(mModelViewMatrix, rotation * Math.PI/180, [0.0, 1.0, 0.0], mModelViewMatrix);		
		mat4.multiply(mProjectionMatrix, mModelViewMatrix, mvp);
								
        var drawCall = new DrawCall();
        drawCall.vbo = renderModel.getRenderMesh().getVertexBufferHandle();
        drawCall.shaderProgram = litShaderProgram;
        drawCall.verticesNumber = 3;

		drawCall.matrixMVP = mvp;
		drawCall.mvpLocation = renderer.getGfx().getUniformLocation(litShaderProgram, "modelViewProjectionMatrix");

        renderer.render(0, drawCall);
        window.requestAnimationFrame(runLoop);
	};

	var onResizeEvent = function () {
	    mat4.perspective(45, mCanvas.clientWidth / mCanvas.clientHeight, 0.1, 100, mProjectionMatrix);
	}

    return this;
}

function SparkPreviewerMain() {

    var APPLICATION = new Application(document.getElementById("sparkViewer"));
    APPLICATION.init();
    APPLICATION.run();

    return 0;
}

