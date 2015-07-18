console.log("ShapeGenetator.js included");

var ShapeGenerator = {};

ShapeGenerator.createTriangle = function() {
    var vertices = [
        0.0, 1.0, 0.0,
        -1.0, -1.0, 0.0,
        1.0, -1.0, 0.0
    ];

    return vertices;
};

ShapeGenerator.createCube = function() {

};