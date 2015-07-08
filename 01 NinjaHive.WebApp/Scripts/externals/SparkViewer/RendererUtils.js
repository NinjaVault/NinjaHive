console.log("RenderUtils included");

function loadTextureFromUrl(url, gfx) {
    var image = new Image();
    var result;

    image.onload = function () {
        result = initTextureFromImage(this, gfx);
        return result;
    }
    
    image.src = url;
}

function initTextureFromImage(image, gfx) {
    var textureHandle = gfx.createTexture();
    gfx.pixelStorei(gfx.UNPACK_FLIP_Y_WEBGL, true);

    gfx.bindTexture(gfx.TEXTURE_2D, textureHandle);

    gfx.texImage2D(gfx.TEXTURE_2D, 0, gfx.RGBA, gfx.RGBA, gfx.UNSIGNED_BYTE, image);
    gfx.texParameteri(gfx.TEXTURE_2D, gfx.TEXTURE_MAG_FILTER, gfx.LINEAR);
    gfx.texParameteri(gfx.TEXTURE_2D, gfx.TEXTURE_MIN_FILTER, gfx.LINEAR);

    gfx.texParameteri(gfx.TEXTURE_2D, gfx.TEXTURE_WRAP_S, gfx.CLAMP_TO_EDGE);
    gfx.texParameteri(gfx.TEXTURE_2D, gfx.TEXTURE_WRAP_T, gfx.CLAMP_TO_EDGE)

    gfx.bindTexture(gfx.TEXTURE_2D, null);
    
    return textureHandle;
}

function initShaderFromString(vertexShaderString, fragmentShaderString, gfx) {
    var shaderProgramHandle = gfx.createProgram();
    
    var vertexShaderHandle = gfx.createShader(gfx.VERTEX_SHADER);
    gfx.shaderSource(vertexShaderHandle, vertexShaderString);
    gfx.compileShader(vertexShaderHandle);
    
    if (!gfx.getShaderParameter(vertexShaderHandle, gfx.COMPILE_STATUS)) {
        alert("ERROR IN VERTEX SHADER : " + gfx.getShaderInfoLog(vertexShaderHandle));
        return false;
    }

    var fragmentShaderHandle = gfx.createShader(gfx.FRAGMENT_SHADER);
    gfx.shaderSource(fragmentShaderHandle, fragmentShaderString);
    gfx.compileShader(fragmentShaderHandle);
    
    if (!gfx.getShaderParameter(fragmentShaderHandle, gfx.COMPILE_STATUS)) {
        alert("ERROR IN FRAGMENT SHADER : " + gfx.getShaderInfoLog(fragmentShaderHandle));
        return false;
    }

    gfx.attachShader(shaderProgramHandle, vertexShaderHandle);
    gfx.attachShader(shaderProgramHandle, fragmentShaderHandle);
    
    gfx.linkProgram(shaderProgramHandle);
    
    return shaderProgramHandle;
}