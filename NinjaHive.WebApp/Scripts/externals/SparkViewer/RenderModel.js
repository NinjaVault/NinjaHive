function RenderModel() {
    var mRenderMesh;
    var mRenderMaterials = [];

    this.setRenderMesh = function (renderMesh) {
        mRenderMesh = renderMesh;
    };

    this.getRenderMesh = function () {
        return mRenderMesh;
    };

    this.addRenderMaterial = function (renderMaterial) {
        mRenderMaterials.push(renderMaterial);
    };

    this.getRenderMaterial = function (index) {
        return mRenderMaterials[index];
    };

    return this;
}