sampler2D inputSampler : register(s0);
float2 PixelSize : register(c0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = float4(0,0,0,0);

    // 5x5 box blur
    for(int y=-2; y<=2; y++)
    {
        for(int x=-2; x<=2; x++)
        {
            color += tex2D(inputSampler, uv + float2(x,y)*PixelSize);
        }
    }

    color /= 25.0;      // усереднення
    color.a = 1.0;      // встановлюємо альфу повністю непрозору
    return color;
}
