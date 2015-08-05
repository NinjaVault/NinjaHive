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

var ObjLoader = {};

/*
* @param filePath - the file path of the .obj model
* @param callback - the function to call when the model is loaded, 
*                   the result of the loading is passed to the callback
* @param result   - the result of the parsed .obj file
*
* returns true if the filePath exists and is an obj file
*/
ObjLoader.loadObj = function (filePath, callback) {
    JRV.xmlHttpGetRequest(filePath, true, function (result) {
        if (result == null) {
            return false;
        }

        var script = result.split("\n");

        var result = {
            positions: [],
            normals: [],
            uvs: [],

            posIndices: [],
            norIndices: [], 
            uvsIndices: [],

            mtlFileName: "",
            materials: [],
        };

        var faceCount = 0;
        var totalFaces = 0;

        for (var i in script) {
            var line = script[i];

            if (line.substring(0, 2) == "v ") {
                var vertex = line.substring(2).split(" ");
                result.positions.push({
                    x: parseFloat(vertex[0]),
                    y: parseFloat(vertex[1]),
                    z: parseFloat(vertex[2])
                });
            } else if (line.substring(0, 2) == "vt") {
                var vt = line.substring(3).split(" ");
                result.uvs.push({
                    u: parseFloat(vt[0]),
                    v: parseFloat(vt[1])
                });

            } else if (line.substring(0, 2) == "vn") {
                var vn = line.substring(3).split(" ");
                result.normals.push({
                    x: parseFloat(vn[0]),
                    y: parseFloat(vn[1]),
                    z: parseFloat(vn[2])
                });

            } else if (line.substring(0, 2) == "f ") {
                var face = line.substring(2).split(" ");
                var tempCount = 0;

                for (var i in face) {
                    if (face[i] != "") {                        
                        if (face[i].indexOf("/") != -1) {
                            //console.log(face[i]);

                            if (face[i].indexOf("//") != -1) {
                                index = face[i].split("//");
                            } else {
                                index = face[i].split("/");
                                var posIndex = parseInt(index[0]);
                                var uvIndex = parseInt(index[1]);
                                var norIndex = parseInt(index[2]);

                                if (posIndex < 0) {
                                    //console.log(result.positions.length + " - " + posIndex + " = " + (result.positions.length + posIndex));
                                    posIndex = result.positions.length + posIndex;
                                } else {
                                    posIndex -= 1;
                                }

                                if (uvIndex < 0) {
                                    uvIndex = result.uvs.length + uvIndex;
                                } else {
                                    uvIndex -= 1;
                                }

                                if (norIndex < 0) {
                                    norIndex = result.normals.length + norIndex;
                                } else {
                                    norIndex -= 1;
                                }

                                result.posIndices.push(posIndex);
                                result.uvsIndices.push(uvIndex);
                                result.norIndices.push(norIndex);
                            }
                        } else {
                            if (tempCount < 3) {
                                console.log("problem");
                                var posIndex = parseInt(face[i]);

                                if (posIndex < 0) {
                                    posIndex += 1;
                                } else {
                                    posIndex -= 1;
                                }

                                result.posIndices.push(posIndex);
                            }                           
                        }

                        tempCount++;
                    }
                }

                faceCount++;
                totalFaces++;

                result.materials[result.materials.length - 1].endIndex = faceCount;

            } else if (line.substring(0, 6) == "mtllib") {
                result.mtlFileName = line.split(" ")[1];

                var fileName = filePath.replace(/^.*[\\\/]/, '');
                console.log(fileName);

                var path = filePath.replace(fileName, result.mtlFileName);
                result.mtlFileName = path;

            } else if (line.substring(0, 6) == "usemtl") {
                faceCount = 0;
                var temp = { id: null, startIndex: null, endIndex: null, diffuseTextureId: null, };
                temp.id = line.split(" ")[1];
                temp.startIndex = totalFaces;

                result.materials.push(temp);
            }
        }

        console.log("Obj loaded\n Path: " + filePath + "\nTrisCount: " + result.posIndices.length + "\nMaterialsCount: " + result.materials.length);
        ObjLoader.loadMtl(result.mtlFileName, result, callback);      
        //callback(result);
        return true;
    });
};

ObjLoader.loadMtl = function (filePath, objModel, callback) {
    JRV.xmlHttpGetRequest(filePath, true, function (result) {
        if (result == null) {
            return false;
        }

        var script = result.split("\n");
        var tempMaterials = [];
        var material = { id: null, diffuseTextureId: null, opacity: null};
        
        for (var i in script) {
            var line = script[i];

            if (line.substring(0, 6) == "newmtl") {
                material.id = line.split(" ")[1];

            } else if (line.substring(0, 2) == "d ") {
                material.opacity = parseFloat(line.split(" ")[1]);
            } else if (line.substring(0, 6) == "map_Kd") {

                material.diffuseTextureId = line.split(" ")[1];
                var fileName = filePath.replace(/^.*[\\\/]/, '');

                var path = filePath.replace(fileName, material.diffuseTextureId);
                material.diffuseTextureId = path;

                for (var i in objModel.materials) {
                    if (objModel.materials[i].id == material.id) {
                        objModel.materials[i].diffuseTextureId = material.diffuseTextureId;
                        objModel.materials[i].opacity = material.opacity;
                        break;
                    }
                }
            }
 
        }

        callback(objModel);
    });
};