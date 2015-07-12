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

var Vector2 = {};

Vector2.create = function(x, y) {
	var out = new Array(2);
	
	out[0] = x || 0;
	out[1] = y || 0;
	
	return out;
};

Vector2.clone = function(a) {
	var out = new Array(2);
	
	out[0] = a[0];
	out[1] = a[1];
	
	return out;
};

Vector2.copy = function(a, out) {
	out[0] = a[0];
	out[1] = a[1];
	
	return out;
};

Vector2.set = function(x, y, out) {
	out[0] = x;
	out[1] = y;
	
	return out;
};

Vector2.add = function(a, b, out) {
	out[0] = a[0] + b[0];
	out[1] = a[1] + b[1];
	
	return out;
};

Vector2.subtract = function(a, b, out) {
	out[0] = a[0] - b[0];
	out[1] = a[1] - b[1];
	
	return out;
};

Vector2.multiply = function(a, b, out) {
	out[0] = a[0] * b[0];
	out[1] = a[1] * b[1];
	
	return out;
};

Vector2.divice = function(a, b, out) {
	out[0] = a[0] / b[0];
	out[1] = a[1] / b[1];
	
	return out;
};

Vector2.min = function(a, b, out) {
	out[0] = Math.min(a[0], b[0]);
	out[1] = Math.min(a[1], b[1]);
	
	return out;
};

Vector2.max = function(a, b, out) {
	out[0] = Math.max(a[0], b[0]);
	out[1] = Math.max(a[1], b[1]);
	
	return out;
};

Vector2.scale = function(a, scalar, out) {
	out[0] = a[0] * scalar;
	out[1] = a[1] * scalar;
	
	return out;
};

Vector2.distance = function(a, b) {
	var x = b[0] - a[0];
	var y = b[1] - a[1];
	
	return Math.sqrt(x * x + y * y);
};

Vector2.squaredDistance = function(a, b) {
	var x = b[0] - a[0];
	var y = b[1] - a[1];
	
	return x * x + y * y;
};

Vector2.length = function(a) {
	return Math.sqrt(a[0] * a[0] + a[1] * a[1]);
};

Vector2.squaredLength = function(a) {
	return a[0] * a[0] + a[1] * a[1];
};

Vector2.negative = function(a, out) {
	out[0] = -a[0];
	out[1] = -a[1];
	
	return out;
};

Vector2.normalize = function(a, out) {
	var l = Vector2.length(a);
	
	if(l > 0) {
		out[0] = a[0] / l;
		out[1] = a[1] / l;
	}
	
	return out;
};

Vector2.dot = function(a, b) {
	return a[0] * b[0] + a[1] * b[1];
};

Vector2.cross = function(a, b) {
	return a[0] * b[1] - a[1] * b[0];
};

Vector2.cross3D = function(a, b, out) {
	out[0] = 0;
	out[1] = 0;
	
	out[2] = Vector2.cross(a, b);
	return out;
};

Vector2.linearInterpolation = function(a, b, t, out) {
	out[0] = a[0] + t * (b[0] - a[0]);
	out[1] = a[1] + t * (b[1] - a[1]);
	
	return out;
};

Vector2.random = function(scale, out) {
	scale = scale || 1.0;
	
	var r = Math.random() * 2.0 * Math.PI;
	out[0] = Math.cos(r) * scale;
	out[1] = Math.sin(r) * scale;
	
	return out;
};