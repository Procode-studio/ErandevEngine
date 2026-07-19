#version 410 core

in vec2 texCoord;
in vec4 vertexColor;
out vec4 outputColor;

uniform sampler2D texture0;
uniform bool useTexture;

void main()
{
    outputColor = texture(texture0, texCoord);
}