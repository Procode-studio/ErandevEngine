#version 410 core

uniform mat4 model;
uniform mat4 projection;
uniform mat4 view;  

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;
layout (location = 2) in vec2 aTexCoord;

out vec4 vertexColor;
out vec2 texCoord;

void main() {
    gl_Position = projection * view * model * vec4(aPosition, 1.0);
    vertexColor = aColor;
    texCoord = aTexCoord;
}