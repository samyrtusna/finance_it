function Spinner() {
  return (
    <div className="w-dvw h-dvh flex items-center justify-center">
      <div
        id="loader"
        className="relative w-20 h-20 flex items-center justify-center"
      >
        <img
          src="/public/logo.png"
          alt="App Logo"
          className="w-20 h-20"
        />
      </div>
    </div>
  );
}

export default Spinner;
