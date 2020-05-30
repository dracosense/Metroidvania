shader_type canvas_item;



void fragment()
{
	vec2 pos = UV;
	vec2 eye = vec2(0.60f, 0.4f);
	float k = abs(pos.y - eye.y);
	vec4 c = texture(TEXTURE, pos);
	COLOR = c - (0.6f + 0.1f * sin(TIME)) * (2.0f * (abs((pos - eye).x) + abs(pos - eye).y)) * vec4(1, 1, 1, 1);
}

