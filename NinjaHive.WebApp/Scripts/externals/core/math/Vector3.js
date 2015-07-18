// The MIT License (MIT)

// Copyright (c) 2015 Ruggero Enrico Visintin

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE. 

var Vector3 = {};

Vector3.create = function(x, y, z) {
	var out = new Array(3);
	
	out[0] = x || 0;
	out[1] = y || 0;
	out[2] = z || 0;
	
	return out;
};

Vector3.clone = function(a) {
	var out = new Array(3);
	
	out[0] = a[0];
	out[1] = a[1];
	out[2] = a[2];
	
	return out;
};

Vector3.copy = function(a, out) {
	out[0] = a[0];
	out[1] = a[1];
	out[2] = a[2];
	
	return out;	
};

Vector3.set = function(x, y, z, out) {
	out[0] = x;
	out[1] = y;
	out[2] = z;
	
	return out;
};

Vector3.add = function(a, b, out) {
	out[0] = a[0] + b[0];
	out[1] = a[1] + b[1];
	out[2] = a[2] + b[2];
	
	return out;
};

Vector3.subtract = function(a, b, out) {
	out[0] = a[0] - b[0];
	out[1] = a[1] - b[1];
	out[2] = a[2] - b[2];
	
	return out;
};

Vector3.multiply = function(a, b, out) {
	out[0] = a[0] * b[0];
	out[1] = a[1] * b[1];
	out[2] = a[2] * b[2];

	return out;
};

Vector3.divide = function(a, b, out) {
	out[0] = a[0] / b[0];
	out[1] = a[1] / b[1];
	out[2] = a[2] / b[2];
	
	return out;
};

Vector3.min = function(a, b, out) {
	out[0] = Math.min(a[0], b[0]);
	out[1] = Math.min(a[1], b[1]);
	out[2] = Math.min(a[2], b[2]);
	
	return out;
};

Vector3.max = function(a, b, out) {
	out[0] = Math.max(a[0], b[0]);
	out[1] = Math.max(a[1], b[1]);
	out[2] = Math.max(a[2], b[2]);
	
	return out;
	
};

Vector3.scale = function(a, scalar, out) {
	out[0] = a[0] * scalar;
	out[1] = a[1] * scalar;
	out[2] = a[2] * scalar;
	
	return out;
};

Vector3.distance = function(a, b) {
	var x = b[0] - a[0];
	var y = b[1] - a[1];
	var z = b[2] - a[2];
	
	return Math.sqrt(x * x + y * y + z * z);
};

Vector3.squaredDistance = function(a, b) {
	var x = b[0] - a[0];
	var y = b[1] - a[1];
	var z = b[2] - a[2];
	
	return x * x + y * y + z * z;
};

Vector3.length = function(a) {
	return Math.sqrt(a[0] * a[0] + a[1] * a[1] + a[2] * a[2]);
};

Vector3.squaredLength = function(a) {
	return a[0] * a[0] + a[1] * a[1] + a[2] * a[2];
};

Vector3.negative = function(a, out) {
	out[0] = -a[0];
	out[1] = -a[1];
	out[2] = -a[2];
	
	return out;
};

Vector3.normalize = function(a, out) {
	var l = length(a);
	
	if(l > 0) {
		out[0] = a[0] / l;
		out[1] = a[1] / l;
		out[2] = a[2] / l;
	}
	
	return out;
};

Vector3.dot = function(a, b) {
	return a[0] * b[0] + a[1] * b[1] + a[2] * b[2];
};

Vector3.cross = function(a, b, out) {
	out[0] = a[1] * b[2] - a[2] * b[1];
	out[1] = a[2] * b[0] - a[0] * b[2];
	out[2] = a[0] * b[1] - a[1] * b[0];
	
	return out;
};

Vector3.linearInterpolation = function(a, b, t, out) {
	out[0] = a[0] + t * (b[0] - a[0]);
	out[1] = a[1] + t * (b[1] - a[1]);
	out[2] = a[2] + t * (b[2] - a[2]);
	
	return out;
};

Vector3.random = function(out, scale) {
	scale = scale || 1.0;
	
	var r = Math.random() * 2.0 * Math.PI;
    var z = (Math.random() * 2.0) - 1.0;
	
	var zScale = Math.sqrt(1.0 - z * z) * scale;
	
	out[0] = Math.cos(r) * zScale;
	out[1] = Math.sin(r) * zScale;
	out[2] = z * zScale;
	
	return out;	
};