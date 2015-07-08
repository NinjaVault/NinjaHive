console.log("RenderMaterial.js included");

function RenderMaterial() { 
    var mDiffuseTextureHandle;

    var mDiffuseColor;
    var mStartIndex;

    this.setDiffuseColor = function (vector3) {
        mDiffuseColor = vector3;
    };

    this.getMaterialColor = function () {
        return mDiffuseColor;
    };

    this.setStartIndex = function (startIndex) {
        mStartIndex = startIndex;
    };

    this.getStartIndex = function () {
        return mStartIndex;
    };

    this.setDiffuseTextureHandle = function (diffuseTextureHandle) {
        mDiffuseTextureHandle = diffuseTextureHandle;
    };
    
    this.getDiffsueTextureHandle = function() {
     return mDiffuseTextureHandle;
    };
    
    return this;
}