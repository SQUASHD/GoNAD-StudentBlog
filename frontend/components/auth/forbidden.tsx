export default function Forbidden() {
  return (
    <div className="w-screen h-full flex flex-col items-center justify-center">
      <h1 className="text-4xl font-bold">Access Denied</h1>
      <p className="text-xl">You do not have permission to view this page.</p>
    </div>
  );
}
