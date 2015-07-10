function Vector2() {
    
}

Vector2.prototype = {
    create: function (x, y) {
        return [x, y];
    }, 
    
    scalar: function(scalar, out) {
        out[0] *= scalar;
        out[1] *= scalar;
    },
    
    dot: function (a, b, out) {
        return a[0] * b[0] + a[1] * b[1];
    },
    
    length: function (in) {
        return Math.sqrt(in[0] * in[0] + in[1] + in [1]);
    },
    
    normalize: function (in, out) {
        var l = length(in);
        
        out[0] = in[0]/l;
        out[1] = in[1]/l;
    },
    
    
};