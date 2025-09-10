
import { toast } from "react-toastify";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import type { LoginCre } from "../../types/auth";
import { authService } from "../../services/authService";
import FormInput from "../../components/FormInputs";


const LoginPage: React.FC = () => {
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<LoginCre>();
  const navigate = useNavigate();

  const onSubmit = async (data: LoginCre) => {
    try {
      const response = await authService.login(data);
      authService.saveToken(response);
      toast.success("Đăng nhập thành công!");
      navigate("/");
    } catch (error) {
      if (error instanceof Error) {
        toast.error(error.message);
      } else {
        toast.error("Đã xảy ra lỗi không xác định!");
      }
    }
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-cover bg-center" style={{ backgroundImage: `url('https://bcp.cdnchinhphu.vn/Uploaded/hoangtrongdien/2020_04_07/thu%20vien.jpg')` }}>
      <div className="absolute inset-0 bg-black/60" />
      <div className="relative z-10 w-full max-w-md mx-auto bg-white/20 backdrop-blur-lg rounded-2xl shadow-2xl p-10 flex flex-col items-center">
        <h1 className="text-4xl font-extrabold text-white mb-8 drop-shadow text-center tracking-wide">Đăng Nhập</h1>
        <form onSubmit={handleSubmit(onSubmit)} className="w-full space-y-5">
          <FormInput
            id="username"
            label="Tên đăng nhập*"
            type="text"
            register={register("username", { required: "Tên đăng nhập là bắt buộc" })}
            error={errors.username}
          />
          <FormInput
            id="password"
            label="Mật khẩu*"
            type="password"
            register={register("password", {
              required: "Mật khẩu là bắt buộc",
              minLength: { value: 6, message: "Mật khẩu phải có ít nhất 6 ký tự" },
            })}
            error={errors.password}
          />
          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full py-3 rounded-lg bg-gradient-to-r from-blue-600 to-blue-400 text-white font-semibold text-lg shadow-md hover:from-blue-700 hover:to-blue-500 transition-all duration-300 disabled:opacity-60"
          >
            {isSubmitting ? "Đang đăng nhập..." : "Đăng Nhập"}
          </button>
        </form>
        <p className="mt-8 text-center text-white text-base">
          Chưa có tài khoản?{' '}
          <a href="/register" className="text-blue-200 hover:underline font-medium">Đăng ký</a>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;
