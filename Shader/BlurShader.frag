#version 330 core
out vec4 FragColor;

void main()
{
    float weight[5] = float[](0.227027, 0.1945946, 0.1216216, 0.054054, 0.016216);
    vec3 baseColor = vec3(0.3);
    vec3 color = vec3(0.0);

    for(int i=-4;i<=4;i++)
    {
        int idx = abs(i);
        if(idx>4) idx=4;
        color += baseColor * weight[idx];
    }

    FragColor = vec4(color, 1.0);
}  