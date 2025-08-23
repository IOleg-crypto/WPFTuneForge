using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Wpf;
using System;
using System.IO;
using System.Windows.Media;

namespace WpfTuneForgePlayer.Shader
{
    public class ShaderGL
    {
        public GLWpfControlSettings Settings { get; private set; }
        private bool _initialized = false;

        public int ShaderProgram { get; private set; }
        public int Vao { get; private set; }
        public int Vbo { get; private set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public ShaderGL()
        {
            Settings = new GLWpfControlSettings { MajorVersion = 3, MinorVersion = 6 };
        }

        public void GlControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string baseDir = "D:/gitnext/WpfTuneForgePlayer/";
            string vertPath = Path.Combine(baseDir, "Shader", "BlurShader.vert");
            string fragPath = Path.Combine(baseDir, "Shader", "BlurShader.frag");

            if (!File.Exists(vertPath) || !File.Exists(fragPath))
                throw new Exception("Shader files not found!");

            ShaderProgram = CreateShader(vertPath, fragPath);

            float[] vertices = { -1f,-1f,0f,  1f,-1f,0f,  1f,1f,0f,
                                 1f,1f,0f,  -1f,1f,0f,  -1f,-1f,0f };

            Vao = GL.GenVertexArray();
            Vbo = GL.GenBuffer();

            GL.BindVertexArray(Vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            _initialized = true;
        }

        public void GlControl_Render(TimeSpan delta)
        {
            if (!_initialized) return;

            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(ShaderProgram);
            GL.BindVertexArray(Vao);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        private int CreateShader(string vertPath, string fragPath)
        {
            string vertSource = File.ReadAllText(vertPath);
            string fragSource = File.ReadAllText(fragPath);

            int vertShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertShader, vertSource);
            GL.CompileShader(vertShader);
            GL.GetShader(vertShader, ShaderParameter.CompileStatus, out int vertStatus);
            if (vertStatus == 0) throw new Exception("Vertex shader compile failed: " + GL.GetShaderInfoLog(vertShader));

            int fragShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragShader, fragSource);
            GL.CompileShader(fragShader);
            GL.GetShader(fragShader, ShaderParameter.CompileStatus, out int fragStatus);
            if (fragStatus == 0) throw new Exception("Fragment shader compile failed: " + GL.GetShaderInfoLog(fragShader));

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
