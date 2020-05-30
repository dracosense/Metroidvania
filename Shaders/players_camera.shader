shader_type canvas_item;

void fragment()
{
	vec2 pos = UV;
	vec2 center = vec2(0.5f, 0.5f);
	vec2 v = abs(pos - center);
	COLOR = (3.0f + 0.2f * cos(0.f * TIME)) * abs(v.x * v.x + v.y * v.y) * vec4(0, 0, 0, 1);
}