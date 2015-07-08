console.log("Renderer.js included");

function DrawCall() {
    this.shader;
    this.vao;
    this.textureHandle;

    this.verticesNumber;
	this.matrixMVP;
	
	this.mvpLocation;
}

function Renderer() {
    var _status = this;
    var mGfx;

    this.program;

    var positionsAttribLocation 		= 0;
    var normalsAttrbLocation 			= 1;
    var uvsAttribLocation 				= 2;

    this.getGfx = function() {
        return mGfx;
    };
   
    this.init = function () {
        mGfx.clearColor(0.0, 0.0, 0.0, 1.0);      

        mGfx.enableVertexAttribArray(positionsAttribLocation);
		//mGfx.enableVertexAttribArray(normalsAttrbLocation);
        mGfx.enableVertexAttribArray(1);

        console.log(positionsAttribLocation);
        console.log(uvsAttribLocation);
    };

    this.render = function (deltaTime, drawCall) {
        mGfx.clear(mGfx.COLOR_BUFFER_BIT);
        mGfx.useProgram(drawCall.shaderProgram);
			
        mGfx.bindBuffer(mGfx.ARRAY_BUFFER, drawCall.vbo);

        mGfx.vertexAttribPointer(positionsAttribLocation, 2, mGfx.FLOAT, false, 4 * 4, 0);
		//mGfx.vertexAttribPointer(normalsAttrbLocation, 3, mGfx.FLOAT, false, 4 * 7, 0);
        mGfx.vertexAttribPointer(1, 2, mGfx.FLOAT, false, 4 * 4, 2 * 4);

		mGfx.uniformMatrix4fv(drawCall.mvpLocation, false, drawCall.matrixMVP);

		
        mGfx.drawArrays(mGfx.TRIANGLES, 0, drawCall.verticesNumber);
        mGfx.bindBuffer(mGfx.ARRAY_BUFFER, null);

        mGfx.flush();
    };

    // initialization 
    this.initWebGL = function (canvas) {
        try {
            mGfx = canvas.getContext("webgl") || canvas.getContext("experimental-webgl");
        } catch (e) { }

        if (!mGfx || mGfx == undefined) {
            _status = null;

			console.log(canvas);
			
            return;
        }
		
		
    };

    return _status;	
}
