import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import type { LoginCre } from "../../types/auth";
import { authService } from "../../services/authService";
import FormInput from "../../components/FormInputs";

const LoginPage: React.FC = () => {
  const {register,handleSubmit,formState: { errors, isSubmitting },} = useForm<LoginCre>();
  const navigate = useNavigate();

  const onSubmit = async (data: LoginCre) => {
    try {
      const response = await authService.login(data);
      authService.saveToken(response);
      toast.success("Đăng nhập thành công!");
      navigate("/");
    } catch (error) {
        if (error instanceof Error) {
    toast.error(error.message); // Hiển thị đúng thông báo từ backend
  } else {
    toast.error('Đã xảy ra lỗi không xác định!');
  }
    }
}
    return (
  <div className="min-h-screen flex items-center justify-center px-4 relative">
    <div
      className="absolute inset-0 bg-cover bg-center bg-no-repeat"
      style={{
        backgroundImage: `url('https://bcp.cdnchinhphu.vn/Uploaded/hoangtrongdien/2020_04_07/thu%20vien.jpg')`,
      }}
    />
    <div className="absolute inset-0 bg-black/60" />

    <div className="relative z-10 w-full max-w-md bg-white/20 backdrop-blur-md rounded-xl shadow-lg p-8">
      <h1 className="text-4xl font-bold text-white mb-6 text-center">Đăng Nhập</h1>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <FormInput
          id="username"
          label="Tên đăng nhập*"
          type="text"
          register={register("username", {
            required: "Tên đăng nhập là bắt buộc",
          })}
          error={errors.username}
        />
        <FormInput
          id="password"
          label="Mật khẩu*"
          type="password"
          register={register("password", {
            required: "Mật khẩu là bắt buộc",
          })}
          error={errors.password}
        />
        <button
          type="submit"
          disabled={isSubmitting}
          className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 disabled:bg-blue-400 transition duration-300"
        >
          {isSubmitting ? "Đang đăng nhập..." : "Đăng Nhập"}
        </button>
      </form>

      <p className="mt-6 text-center text-white">
        Chưa có tài khoản?{" "}
        <a href="/register" className="text-blue-300 hover:underline">
          Đăng ký
        </a>
      </p>
    </div>
  </div>
);
};

export default LoginPage;