shader_type canvas_item;

uniform float speed_k = 3;

uniform bool out_of_game = false;

uniform bool spirit_f = false;

void fragment()
{
	COLOR = texture(TEXTURE, UV);
	if (spirit_f)
	{
		COLOR.rgb = vec3(0, 0, 0);
	}
	if (out_of_game)
	{
		float a = 0.5 * cos(speed_k * TIME);
		if (spirit_f)
		{
			a += 0.5;
		}
		COLOR.rgb += vec3(a, a, a);
	}
}