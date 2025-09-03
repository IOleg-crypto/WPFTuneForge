using OpenTK.Graphics.OpenGL;
using OpenTK.Wpf;
using System;
using System.IO;

namespace WpfTuneForgePlayer.Shader
{
    public class ShaderGL
    {
        public GLWpfControlSettings Settings { get; private set; }
        private bool _initialized = false;

        public int ShaderProgram { get; private set; }
        public int Vao { get; private set; }
        public int Vbo { get; private set; }

        private int textureId;
        private int fbo1, fbo2;
        private int texFbo1, texFbo2;
        private int horizontalUniform;

        public float Width { get; set; } = 800;
        public float Height { get; set; } = 600;

        public ShaderGL()
        {
            Settings = new GLWpfControlSettings
            {
                MajorVersion = 3,
                MinorVersion = 6,
            };
        }

        public void GlControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string vertPath = Path.Combine(baseDir, "Shader", "BlurShader.vert");
            string fragPath = Path.Combine(baseDir, "Shader", "BlurShader.frag");

            if (!File.Exists(vertPath) || !File.Exists(fragPath))
                throw new Exception("Shader files not found!");

            ShaderProgram = CreateShader(vertPath, fragPath);
            GL.UseProgram(ShaderProgram);

            horizontalUniform = GL.GetUniformLocation(ShaderProgram, "horizontal");

            // --- 2. Створюємо квад ---
            float[] vertices = {
                // pos         tex
                -1f,-1f,0f,   0f,0f,
                 1f,-1f,0f,   1f,0f,
                 1f, 1f,0f,   1f,1f,
                 1f, 1f,0f,   1f,1f,
                -1f, 1f,0f,   0f,1f,
                -1f,-1f,0f,   0f,0f
            };

            Vao = GL.GenVertexArray();
            Vbo = GL.GenBuffer();
            GL.BindVertexArray(Vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            // --- 3. Генеруємо procedural texture ---
            textureId = GenerateProceduralTexture(256, 256);

            // --- 4. Створюємо FBO для двопрохідного blur ---
            fbo1 = GL.GenFramebuffer();
            fbo2 = GL.GenFramebuffer();
            texFbo1 = CreateEmptyTexture((int)Width, (int)Height);
            texFbo2 = CreateEmptyTexture((int)Width, (int)Height);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo1);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texFbo1, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo2);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texFbo2, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            _initialized = true;
        }

        public void GlControl_Render(TimeSpan delta)
        {
            if (!_initialized) return;

            // --- Горизонтальний проход ---
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo1);
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(ShaderProgram);
            GL.Uniform1(horizontalUniform, 1); // horizontal = true
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            // --- Вертикальний проход ---
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Uniform1(horizontalUniform, 0); // horizontal = false
            GL.BindTexture(TextureTarget.Texture2D, texFbo1);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        private int GenerateProceduralTexture(int width, int height)
        {
            byte[] data = new byte[width * height * 3];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = (y * width + x) * 3;
                    byte value = (byte)(((x / 32 + y / 32) % 2) * 255);
                    data[i] = value;
                    data[i + 1] = value;
                    data[i + 2] = value;
                }
            }

            int texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0,
                          PixelFormat.Rgb, PixelType.UnsignedByte, data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            return texId;
        }

        private int CreateEmptyTexture(int width, int height)
        {
            int tex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tex);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0,
                          PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            return tex;
        }

        private int CreateShader(string vertPath, string fragPath)
        {
            string vertSource = File.ReadAllText(vertPath);
            string fragSource = File.ReadAllText(fragPath);

            int vertShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertShader, vertSource);
            GL.CompileShader(vertShader);
            GL.GetShader(vertShader, ShaderParameter.CompileStatus, out int vertStatus);
            if (vertStatus == 0)
                throw new Exception("Vertex shader compile failed: " + GL.GetShaderInfoLog(vertShader));

            int fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragShader, fragSource);
            GL.CompileShader(fragShader);
            GL.GetShader(fragShader, ShaderParameter.CompileStatus, out int fragStatus);
            if (fragStatus == 0)
                throw new Exception("Fragment shader compile failed: " + GL.GetShaderInfoLog(fragShader));

            int program = GL.CreateProgram();
            GL.AttachShader(program, vertShader);
            GL.AttachShader(program, fragShader);
            GL.LinkProgram(program);

            GL.DeleteShader(vertShader);
            GL.DeleteShader(fragShader);

            return program;
        }
    }
}
