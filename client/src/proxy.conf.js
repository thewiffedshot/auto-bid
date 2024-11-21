var defaultTarget = 'http://localhost:5013';

module.exports = [
{
   context: ['/api/**'],
   target: defaultTarget,
   changeOrigin: true,
}
];