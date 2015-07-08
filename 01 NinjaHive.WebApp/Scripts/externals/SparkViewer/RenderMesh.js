console.log("RenderMesh.js included");

function RenderMesh() {
  var mVertexBufferHandle;  
  var mVerticesSet;

  this.setVerticesSet = function (verticesSet) {
      mVerticesSet = verticesSet;
  };

  this.setVertexBufferHandle = function (vertexBufferHandle) {
      mVertexBufferHandle = vertexBufferHandle;
  };

  this.getVerticesSet = function() { 
   return mVerticesSet;
  };

  this.getVertexBufferHandle = function() { 
   return mVertexBufferHandle;
  };

  return this;
}